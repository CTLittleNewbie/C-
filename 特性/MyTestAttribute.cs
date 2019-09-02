using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 特性
{
    //特性类的结尾一般以Attribute结尾
    //需要继承System.Attribute
    //一般不会被继承，加上sealed关键字
    //一般情况下特性类用来表示目标结构的一些状态（定义一些字段或属性，一般不定义方法）

    [AttributeUsage(AttributeTargets.Class)]//定义这个特性类使用的目标是类 
    sealed class MyTestAttribute:System.Attribute
    {
        public string Description { set; get; }
        public string VersionNumber { set; get; }
        public int ID { set; get; }

        public MyTestAttribute(string descriptiond)
        {
            Description = descriptiond;
        }

    }
}
