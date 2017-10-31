using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Message
    {
        private int _messageId;
        private string _fromUserId;
        private string _toUserId;
        private int _flatId;
        private string _description;
        private bool _isHidden;

        public virtual ApplicationUser FromUser { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
        public virtual Flat Flat { get; set; }


        public int MessageId
        {
            get
            {
                return _messageId;
            }

            set
            {
                _messageId = value;
            }
        }

        public string FromUserId
        {
            get
            {
                return _fromUserId;
            }

            set
            {
                _fromUserId = value;
            }
        }

        public string ToUserId
        {
            get
            {
                return _toUserId;
            }

            set
            {
                _toUserId = value;
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

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public bool IsHidden
        {
            get
            {
                return _isHidden;
            }

            set
            {
                _isHidden = value;
            }
        }
    }
}