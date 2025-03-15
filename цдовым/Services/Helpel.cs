using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

//using WpfApp1.Models;
using цдовым.Models;

namespace WpfApp1
{
    internal class Helpel
    {
        private static bazaEntities _context; // Статическая приватная переменная для хранения контекста модели данных

        /// <summary>
        /// Метод для получения контекста данных, необходимого для подключения к базе данных.
        /// </summary>
        /// <returns>Контекст данных типа demonEntities.</returns>
        //public static bazaEntities GetContext()
        //{
        //    // Проверка, установлено ли подключение; если нет, создается новое подключение
        //    if (_context == null)
        //    {
        //        _context = new bazaEntities(); // Создание нового подключения к БД
        //    }
        //    return _context; // Возвращение текущего подключения
        //}
       //private static bazaEntities _context;

            public static bazaEntities GetContext()
            {
                if (_context == null)
                {
                    _context = new bazaEntities();
                }
                return _context;
            }
        

        /// <summary>
        /// Метод для добавления новой записи о пользователе в таблицу Users базы данных.
        /// </summary>
        /// <param name="user">Объект типа Users, содержащий информацию о новом пользователе.</param>
        public void CreateSoiskatel(soiskateli user)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            _context.soiskateli.Add(user); // Добавление записи нового пользователя в таблицу Users
            _context.SaveChanges(); // Сохранение изменений в БД
        }
        public void CreateSotrudnik(Sotrudniki user)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            _context.Sotrudniki.Add(user); // Добавление записи нового пользователя в таблицу Users
            _context.SaveChanges(); // Сохранение изменений в БД

        }

        public void CreateAuthorization(authorization auth)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }


            _context.authorization.Add(auth); // Добавление записи нового пользователя в таблицу Users

            _context.SaveChanges(); // Сохранение изменений в БД
        }

        /// <summary>
        /// Метод для обновления записи о пользователе в таблице Users базы данных.
        /// </summary>
        /// <param name="user">Объект типа Users, представляющий измененную сущность пользователя.</param>
        public void Updatesoiskateli(soiskateli user)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            // Состояние сущности помечается как Измененная
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges(); // Сохранение измененной сущности в БД
        }

        /// <summary>
        /// Метод для удаления записи о пользователе из таблицы Users базы данных.
        /// </summary>
        /// <param name="idUser">Целое число, представляющее идентификатор 
        /// пользователя, который необходимо удалить.</param>
        public void RemoveUser(int idUser)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            var users = _context.Sotrudniki.Find(idUser); // Поиск записи пользователя по его id
            _context.Sotrudniki.Remove(users); // Удаление записи найденного пользователя
            _context.SaveChanges(); // Сохранение изменений в БД
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int GetLastAuthorizationId()
        {
            if (_context == null)
            {
                _context = GetContext();
            }
            var lastAuth = _context.authorization.OrderByDescending(a => a.id_authorization).FirstOrDefault();
            if (lastAuth != null)
            {
                return Convert.ToInt32(lastAuth.id_authorization);
            }
            throw new InvalidOperationException("Таблица Авторизация пуста.");
        }
        public int GetLastEmloyesId()
        {
            if (_context == null)
            {
                _context = GetContext();
            }
            var lastAuth = _context.Sotrudniki.OrderByDescending(a => a.id_Sotrudnik).FirstOrDefault();
            if (lastAuth != null)
            {
                return Convert.ToInt32(lastAuth.id_authorization);
            }
            throw new InvalidOperationException("Таблица Авторизация пуста.");
        } 
        public int GetLastOtklikId()
        {
            if (_context == null)
            {
                _context = GetContext();
            }
            var lastAuth = _context.Otklki_Soskateley.OrderByDescending(a => a.id_otklil).FirstOrDefault();
            if (lastAuth != null)
            {
                return Convert.ToInt32(lastAuth.id_otklil);
            }
            throw new InvalidOperationException("Таблица Авторизация пуста.");
        }

        public void CreateOtklik(int soiskatelId, int vakansiId, int statusId)
        {
            // Убедитесь, что контекст инициализирован
            if (_context == null)
            {
                _context = GetContext();
            }

            // Создаем новый объект отклика
            var otklik = new Otklki_Soskateley
            {
                id_soiskatel = soiskatelId,
                id_vakansi = vakansiId,
                id_status = statusId,
                id_otklil=GetLastOtklikId()+1,
                data_otklik = DateTime.Now // Текущая дата и время
            };

            // Добавляем отклик в контекст
            _context.Otklki_Soskateley.Add(otklik);

            // Сохраняем изменения в базе данных
            _context.SaveChanges();
        }

        /// <summary>
        /// Метод для фильтрации записей пользователей по имени.
        /// </summary>
        /// <returns>Список пользователей, чьи имена начинаются с буквы 'M' или 'A'.</returns>
        //public List<Sotrudniki> FiltrUsers()
        //{
        //    // Убедитесь, что контекст инициализирован
        //    if (_context == null)
        //    {
        //        _context = GetContext();
        //    }

        //    //return _context.s.Where(x => x.Имя.StartsWith("M") || x.Имя.StartsWith("A")).ToList();
        //}

        /// <summary>
        /// Метод для сортировки записей пользователей по имени.
        /// </summary>
        /// <returns>Список пользователей, отсортированных по именам в порядке возрастания.</returns>
        //    public List<Клиент> SortUsers()
        //    {
        //        // Убедитесь, что контекст инициализирован
        //        if (_context == null)
        //        {
        //            _context = GetContext();
        //        }

        //        return _context.Клиент.OrderBy(x => x.Имя).ToList();
        //    }
        //}
    }
}
