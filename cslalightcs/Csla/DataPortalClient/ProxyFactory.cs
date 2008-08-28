﻿using System;

namespace Csla.DataPortalClient
{
  public class ProxyFactory
  {
    protected internal virtual IDataPortalProxy<T> GetProxy<T>() where T : Csla.Serialization.Mobile.IMobileObject
    {
      return GetProxy<T>(DataPortal.ProxyModes.Auto);
    }

    protected internal virtual IDataPortalProxy<T> GetProxy<T>(DataPortal.ProxyModes proxyMode) 
      where T : Csla.Serialization.Mobile.IMobileObject
    {
      if (proxyMode == DataPortal.ProxyModes.LocalOnly || Csla.DataPortal.ProxyTypeName == "Local")
      {
        return new LocalProxy<T>();
      }
      else
      {
        Type proxyType = Type.GetType(Csla.DataPortal.ProxyTypeName);
        Type generixProxyType = proxyType.MakeGenericType(typeof(T));
        return (IDataPortalProxy<T>)Activator.CreateInstance(generixProxyType);
      }
    }
  }
}
