using Service.History.Core.Framework.Navigation.Class;
using Service.History.Core.Pattern.Proxy.Interface;

namespace Service.History.Core.Pattern.Proxy.Class
{
    public class ProxyPages : IProxyPages
    {
        #region Properties
        private readonly Pages _navigationPage;
        public string Title
        {
            get => this._navigationPage.Title;
            set => this._navigationPage.Title = value;
        }
        #endregion

        #region Constructor
        public ProxyPages()
        {
            this._navigationPage = new Pages();
        }
        #endregion

        #region Public Methods
        public string Controller<T>() where T : class
        {
            return this._navigationPage.Controller<T>();
        }

        public string Area<T>() where T : class
        {
            return this._navigationPage.Area<T>();
        }
        #endregion
    }
}
