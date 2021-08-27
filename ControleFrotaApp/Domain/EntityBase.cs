using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFrota.Domain
{
    /// <summary>
    /// Classe herdável que expõe uma chave ID (int), além de INotifyPropertyChanged
    /// </summary>
    public abstract class EntityBase : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class ExtendedEntityBase : EntityBase, IDataErrorInfo
    {
        [NotMapped]
        public string Error => throw new NotImplementedException();

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (!string.IsNullOrWhiteSpace(columnName))
                {
                    if (string.IsNullOrWhiteSpace((string)GetType().GetProperty(columnName).GetValue(this)))
                    {
                        result = $"O campo {columnName} não pode ser em branco";
                        return result;
                    }

                }
                return null;

            }
        }
    }
}