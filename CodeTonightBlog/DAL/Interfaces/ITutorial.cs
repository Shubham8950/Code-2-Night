using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTonightBlog.DAL.Common;
 

namespace CodeTonightBlog.DAL.Interfaces
{
   public  interface ITutorial
    {
        void AddTutorials(Tutorial blog);
        List<Tutorial> ListTutorial(string technology);
        List<Tutorial> gettutorials();
    }
}
