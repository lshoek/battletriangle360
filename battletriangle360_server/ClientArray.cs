using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiClientGameServer
{
    class ClientArray
    {
        Client[] arr;
        int maxSize;
        int presentClients;
        string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange", "Grey", "Black" };

        public ClientArray(int size)
        {
            this.maxSize = size;
            arr = new Client[maxSize];
        }

        public void Add(Client c)
        {
            if (!IsFull())
                for (int i = 0; i < maxSize; i++)
                    if (arr[i] == null)
                    {
                        arr[i] = c;
                        c.setColor(colors[i]);
                        presentClients++;
                        break;
                    }
        }

        public int FirstEmptySlot()
        {
            for (int i = 0; i < maxSize; i++)
                if (arr[i] == null)
                    return i;
            return maxSize;
        }

        public void Remove(string userName)
        {
            for (int i = 0; i < maxSize; i++)
                if (arr[i].getUserName() == userName)
                {
                    arr[i] = null;
                    presentClients--;
                    break;
                }
        }

        public bool IsFull()
        {
            for (int i = 0; i < maxSize; i++)
                if (arr[i] == null)
                    return false;
            return true;
        }

        public Client[] getConnectedOnly()
        {
            if (presentClients <= 0)
                return null;

            int ii = 0;
            Client[] connArr = new Client[presentClients];
            for (int i = 0; i < maxSize; i++)
                if (arr[i] != null)
                {
                    connArr[ii] = arr[i];
                    ii++;
                }
                else if (ii == presentClients)
                {
                    break;
                }
            return connArr;
        }

        public Client[] getAll()
        {
            return arr;
        }
    }
}
