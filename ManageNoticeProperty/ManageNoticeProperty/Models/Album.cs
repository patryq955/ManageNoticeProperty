using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Album
    {
        private int _albumId;
        private int _flatId;
        private string _path;
        public virtual Flat Flat { get; set; }

        public int AlbumId
        {
            get
            {
                return _albumId;
            }

            set
            {
                _albumId = value;
            }
        }

        public int FlatId
        {
            get
            {
                return _flatId;
            }

            set
            {
                _flatId = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
            }
        }

    }
}