namespace ControleFrota.Services
{
    public class Messaging<TObject> : IMessaging<TObject>
    {
        public TObject Mensagem { get; set; }
    }
}
