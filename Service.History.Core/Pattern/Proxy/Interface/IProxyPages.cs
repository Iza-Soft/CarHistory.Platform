namespace Service.History.Core.Pattern.Proxy.Interface;

public interface IProxyPages
{
    string Controller<T>() where T : class;
    string Area<T>() where T : class;
    string Title { get; set; }
}