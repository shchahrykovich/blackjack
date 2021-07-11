using System;
using System.Collections.Generic;
using System.Web;

namespace Blackjack.Web.Tests.Fakes
{
    public class FakeHttpSessionStateBase : HttpSessionStateBase
    {
        public readonly Dictionary<String, Object> Storage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get
            {
                if (Storage.ContainsKey(name))
                {
                    return Storage[name];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if(Storage.ContainsKey(name))
                {
                    Storage.Remove(name);
                }
                Storage.Add(name, value);
            }
        }
    }
}
