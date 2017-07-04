using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Models
{
    public class Period
    {

        private DateTime _startDateTime;
        private DateTime _endDateTime;

        public Period(DateTime startDateTime, DateTime endDateTime)
        {
            this._startDateTime = startDateTime;
            this._endDateTime = endDateTime;
        }
    }
}