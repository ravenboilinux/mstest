using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class UserEntity : DataEntity
    {
        public string LoginName { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
