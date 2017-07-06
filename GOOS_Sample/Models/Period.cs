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

        public DateTime EndDate => this._endDateTime;
        public DateTime StartDate => this._startDateTime;

        public string StartMonthString
        {
            get { return this._startDateTime.ToString("yyyy-MM"); }
        }

        public string EndMonthString
        {
            get { return this._endDateTime.ToString("yyyy-MM"); }
        }

        public Period(DateTime startDateTime, DateTime endDateTime)
        {
            this._startDateTime = startDateTime;
            this._endDateTime = endDateTime;
        }
    }
}