using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace MultiClientGame
{
    class BulletList
    {
        private List<Bullet> bullets = new List<Bullet>();
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Bullet> getList()
        {
            return bullets.ToList();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(Bullet b )
        {
            bullets.Add(b);
        }
    }
}
