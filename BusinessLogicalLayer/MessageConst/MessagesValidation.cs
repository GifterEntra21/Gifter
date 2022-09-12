using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.MessageConst
{
    internal static class MessagesValidation
    {
        public const string MESSAGE_ERROR_ID_NULL = "Id não pode ser vazio";
        public const string MESSAGE_ERROR_PASSWORD = "Senha está no formato incorreto, deve conter letras maiusculas, letras minusculas, simbolo especial e numeros";
        public const string MESSAGE_ERROR_PASSWORD_LENGHT = "Senha deve conter entre 8 e 20";
        public const string MESSAGE_ERROR_NAME_LENGHT = "Nome deve conter entre 2 e 30 caracteres";
        public const string MESSAGE_ERROR_LAST_NAME_LENGHT = "Sobrenome deve conter entre 2 e 30 caracteres";
        public const string MESSAGE_ERROR_USERNAME_LENGHT = "Username deve conter entre 5 e 30 caracteres";
    }
}
