using AI.Code.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AI.Code.Base
{
    public class BaseView<T> : WebViewPage<T>
    {

        public new SessionWrapper Session = SessionWrapper.Instance;

        public override void InitHelpers()
        {
            base.InitHelpers();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

    }
}