using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    class Category : IComparable
    {
        private readonly string name;
        private readonly MessageType type;
        private readonly MessageTopic topic;
        public Category(string name, MessageType messageType, MessageTopic messageTopic)
        {
            this.name = name;
            this.type = messageType;
            this.topic = messageTopic;
        }

        public override string ToString()
        {
            return name + "." + type.ToString() + "." + topic.ToString();
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + type.GetHashCode() + type.ToString().GetHashCode() + topic.ToString().GetHashCode() + topic.GetHashCode();
        }

        public int CompareTo(object comparable)
        {
            if (!(comparable is Category cut) ||
                (comparable == null && this == null) ||
                (cut.name == null || this.name == null)
                )
                return 0;
            
            if (name.CompareTo(cut.name) == -1) 
                return -1; 
            
            if (name.CompareTo(cut.name) == 1) return 1;
            
            if (type.CompareTo(cut.type) < 0)
                return -1;

            if (type.CompareTo(cut.type) > 0)
                return 1;

            if (topic.CompareTo(cut.topic) < 0)
                return -1;

            if (topic.CompareTo(cut.topic) > 0) 
                return 1;

            return 0;
        }

        public override bool Equals(object obj)
        {
            return  (
                    this is Category &&
                    obj is Category &&
                    this != null &&
                    obj != null &&
                    this.CompareTo(obj as Category) == 0
                    );
        }

        public static bool operator <= (Category a, Category b)
        {
            return (
                    (a == null && b == null) ||
                    a.CompareTo(b) == -1 || a.CompareTo(b) == 0
                   );
        }

        public static bool operator >= (Category a, Category b)
        {
            return (
                    (a == null && b == null) ||
                    a.CompareTo(b) == 1 || a.CompareTo(b) == 0
                   );
        }

        public static bool operator < (Category a, Category b)
        {
            return (
                    (a != null && b != null) &&
                    a.CompareTo(b) == -1
                   );
        }

        public static bool operator > (Category a, Category b)
        {
            return (
                    (a != null && b != null) &&
                    a.CompareTo(b) == 1
                   );
        }
    }
}
