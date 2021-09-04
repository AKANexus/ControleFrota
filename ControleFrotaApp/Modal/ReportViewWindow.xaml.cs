using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace ControleFrota.Modal
{
    /// <summary>
    /// Interaction logic for ReportViewWindow.xaml
    /// </summary>
    public partial class ReportViewWindow : Window
    {
        private readonly Report _report;
        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;

        public ReportViewWindow(string tempFilePath, Report report)
        {
            _report = report;
            InitializeComponent();

            imageContentPresenter.Content = new Image() { Source = new BitmapImage(new Uri(tempFilePath)) };

            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            scrollViewer.MouseMove += OnMouseMove;

            slider.ValueChanged += OnSliderValueChanged;

        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                scrollViewer.Cursor = Cursors.Hand;
                lastDragPoint = mousePos;
                Mouse.Capture(scrollViewer);
            }
        }

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value += 0.25;
            }
            if (e.Delta < 0)
            {
                slider.Value -= 0.25;
            }

            e.Handled = true;
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.Cursor = Cursors.Arrow;
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }

        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            zoomNumérico.Text = $"{100 * e.NewValue} %";
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;

            var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, grid);
        }

        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
                        Point centerOfTargetNow = scrollViewer.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / grid.Width;
                    double multiplicatorY = e.ExtentHeight / grid.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }


        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new()
            {
                DefaultExt = ".pdf",
                Filter = "Arquivos PDF (.pdf) | *.pdf",
                InitialDirectory = @"%UserProfile%\Documents"
            };
            if (sfd.ShowDialog() == true)
            {
                PDFSimpleExport reportExport = new();
                _report.Export(reportExport, sfd.FileName);
                ProcessStartInfo pi = new ProcessStartInfo
                {
                    FileName = "explorer",
                    Arguments = $"\"{sfd.FileName}\""
                };
                Process.Start(pi);
                this.Close();
            }
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Salve como PDF e imprima no seu visualizador de preferência.");

            //PDFSimpleExport reportExport = new();
            //string pdfTemp = Path.GetTempFileName() + ".pdf";
            //_report.Export(reportExport, pdfTemp);
            //ProcessStartInfo pi = new ProcessStartInfo
            //{
            //    FileName = "explorer",
            //    Arguments = $"\"{pdfTemp}\"",
            //    Verb = "print"
            //};
            //Process.Start(pi);
            //this.Close();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
                slider.Value -= 0.25;
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            slider.Value += 0.25;
        }

        private void ScrollViewer_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!scrollViewer.IsFocused)
            {
                scrollViewer.Focus();
            }
        }
    }
}

