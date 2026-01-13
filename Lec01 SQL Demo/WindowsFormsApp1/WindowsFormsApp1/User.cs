using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class User
    {
        int id;
        string name;

        public void SetId(int id)
        {
            if (0 <= id && id <= 100)
            {
                this.id = id;
            }
            else
                throw new ArgumentException("nu nu nu go 2 school! id ERR!");
        }

        public int GetId() { return id; }

        public int Id
        {
            get { return id; }
            set
            {
                if (0 <= id && id <= 100)
                {
                    id = value;
                }
                else
                    throw new ArgumentException("nu nu nu go 2 school! id ERR!");
            }
        }

        public string Name
        {

            get { return name; }

            set
            {
                if (1 <= value.Length && value.Length <= 10)
                {
                    name = value;
                }
                else
                    throw new ArgumentException("nu nu nu go 2 school! id ERR!");
            }

        }
        public string Family { get; set; }


    }
}
