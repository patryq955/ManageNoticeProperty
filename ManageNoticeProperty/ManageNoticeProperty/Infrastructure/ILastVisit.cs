﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageNoticeProperty.Infrastructure
{
    public interface ILastVisit
    {
        void AddLasstViewProperty(int id);
        IList<string> GetLastViewProperty();



    }
}
