using Service.History.Core.Pattern.Proxy.Interface;

namespace Service.History.Core.Framework.Navigation.Class
{
    internal class Pages : IProxyPages
    {
        #region Properties
        private string _titlePage = null!;
        #endregion

        #region Public Methods
        public string Controller<T>() where T : class
        {
            return this.GetControllerName<T>();
        }
        public string Area<T>() where T : class
        {
            return this.GetAreaName<T>();
        }
        public string Title
        {
            get => this._titlePage;
            set => this._titlePage = value; 
        }
        #endregion

        #region Private Methods
        private string GetControllerName<T>() where T : class
        {
            return typeof(T).Name.Substring(0, typeof(T).Name!.IndexOf("Controller", StringComparison.Ordinal));
        }
        private string GetAreaName<T>() where T : class
        {
            string areaName = string.Empty;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            dynamic attribute = typeof(T).GetCustomAttributesData().SingleOrDefault(item => item.AttributeType.Name == "AreaAttribute");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (attribute != null)
            {
                dynamic arguments = attribute.ConstructorArguments;
                dynamic argument = arguments[0];
                areaName = argument.Value;
            }

            return areaName;
        }
        #endregion
    }
}
