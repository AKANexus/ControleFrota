namespace ControleFrota.Services
{
    public interface IMessaging<TObject>
    {
        public TObject Mensagem { get; set; }
    }
}
