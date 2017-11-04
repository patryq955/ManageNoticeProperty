using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageNoticeProperty.Models;
using ManageNoticeProperty.Models.Repository;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ManageNoticeProperty.Infrastructure
{
    public class LastVisit : ILastVisit
    {
        private HttpCookieCollection _cookies;
        private HttpCookie _cookie;
        private readonly string _nameCookie = "LastVisitPropertyID";
        readonly string[] _nameList = { "first", "second", "third" };
        private List<string> _lastViewPropertyID;

        public LastVisit()
        {
            _cookies = HttpContext.Current.Request.Cookies;
            _lastViewPropertyID = new List<string>();

            LoadCookie();
        }

        public void AddLasstViewProperty(int id)
        {
            _lastViewPropertyID.Insert(0, id.ToString());
            if(_lastViewPropertyID.Count > 3)
            {
                _lastViewPropertyID.RemoveAt(_lastViewPropertyID.Count - 1);
            }

            saveCookie();
        }

        public IList<string> GetLastViewProperty()
        {
            throw new NotImplementedException();
        }

        private void LoadCookie()
        {
            if (_cookies[_nameCookie] == null)
            {
                return;
            }
            foreach (var nameValue in _nameList)
            {
                var value = _cookies[_nameCookie][nameValue] ?? null;
                if (value != null)
                {
                    _lastViewPropertyID.Add(value);
                }

            }

        }

        private void saveCookie()
        {
            int numberName = 0;
            foreach(var propertyID in _lastViewPropertyID)
            {
                if (_cookies[_nameCookie][_nameList[numberName]] == null)
                {
                    _cookie = new HttpCookie(_nameList[numberName]);
                }
                _cookies[_nameCookie][_nameList[numberName]] = propertyID;
                numberName++;
            }
        }

        //public void AddLasstViewProperty(int id)
        //{

        //    _lastViewPropertyID.Add(id.ToString());
        //    if (_lastViewPropertyID.Count > 3)
        //    {
        //        _lastViewPropertyID.RemoveAt(0);
        //    }


        //}

        //public IList<string> GetLastViewProperty()
        //{
        //    return _lastViewPropertyID;
        //}

        //private void LoadCookie()
        //{

        //    foreach (var idCookie in _nameList.Reverse())
        //    {

        //        if (_cookies[idCookie] != null)
        //        {
        //            _lastViewPropertyID.Add(_cookies[idCookie].Value);
        //            break;
        //        }
        //    }
        //}

        //private void saveLastViewProperty()
        //{
        //    int indexNameList = 2;
        //    _lastViewPropertyID.Clear();
        //    foreach (var idProperty in _lastViewPropertyID)
        //    {
        //        HttpCookie cookie;
        //        if (_cookies[_nameList[indexNameList]] == null)
        //        {
        //            cookie = new HttpCookie(_nameList[indexNameList]);
        //        }
        //        cookie[_nameList[indexNameList]] = "ss";
        //    }
        //}
    }
}