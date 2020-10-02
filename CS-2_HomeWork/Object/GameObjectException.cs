using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Object
{
    /// <summary>
    /// Исключения игры
    /// </summary>
    class GameObjectException : Exception
    {
        /// <summary>
        /// Исключение параметров
        /// </summary>
        /// <param name="message">Текст исключения</param>
        public GameObjectException(string message) : base(message)
        {

        }
    }
}
