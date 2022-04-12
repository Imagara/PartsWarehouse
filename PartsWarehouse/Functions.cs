using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PartsWarehouse
{
    public class Functions
    {
        // Валидация номера телефона
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            foreach (char c in phoneNumber)
                if (!char.IsDigit(c))
                    return false;
            if (phoneNumber.Length != 11)
                return false;
            return true;
        }
        // Валидация электронной почты
        public static bool IsValidEmail(string email)
        {
            if (Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                return true;
            else
                return false;
        }
        // Валидация дня рождения
        public static bool IsValidDateOfBirthday(DateTime Date)
        {
            if (Date > DateTime.Now)
                return false;
            else
                return true;
        }
        // Валидация логина и пароля при входе
        public static bool IsValidLogAndPass(string login, string password)
        {
            if (login == "" || password == "")
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsLogEqualPass(string login, string password)
        {
            if (login == password)
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsValidLength(string str)
        {
            if (str.Length < 5)
                return false;
            else
                return true;
        }
        // Проверка на правильность введеных данных при входе
        public static bool LoginCheck(string login, string password)
        {
            if (cnt.db.User.Select(item => item.Login + item.Password).Contains(login + Encrypt.GetHash(password)))
                return true;
            else
                return false;
        }
        // Проверка на уникальность логина
        public static bool IsLoginAlreadyTaken(string login)
        {
            return cnt.db.User.Select(item => item.Login).Contains(login);
        }
        // Проверка на наличие чата
        public static bool IsChatAlreadyCreated(string chatName)
        {
            //return cnt.db.Chat.Select(item => item.Name).Contains(chatName);
            return false;
        }
        // Получение id чата по его названию
        public static int GetIdChat(string chatName)
        {
            //return cnt.db.Chat.Where(item => item.Name == chatName).Select(item => item.IdChat).FirstOrDefault();
            return 0;
        }
        // Проверка на уникальность электронной почты
        public static bool IsEmailAlreadyTaken(string Email)
        {
            return cnt.db.User.Select(item => item.Email).Contains(Email);
        }
        // Проверка на уникальность электронной почты
        public static bool IsPhoneNumberAlreadyTaken(string Phone)
        {
            return cnt.db.User.Select(item => item.PhoneNumber).Contains(Phone);
        }
    }
}
