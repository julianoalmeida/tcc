using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Comum;
using Comum.Exceptions;
using Entidades;
using Entidades.Enums;
using Entidades.Extensions;

namespace _4___Web.Controllers
{
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        public static int LoggedUserProfileAccessCode { get; set; }
        public static string LoggerUserProfileName { get; set; }

        public ActionResult Index()
        {
            var loggedUser = TempData[Constants.LOGGED_USER] as User;

            LoggedUserProfileAccessCode = loggedUser.AccessCode;
            LoggerUserProfileName = loggedUser.Login;
            TempData.Keep(Constants.LOGGED_USER);

            return RedirectToAction("Index", "Home");
        }

        protected string GetSuccessMensagemForSaveOrUpdate(int entityId)
        {
            return entityId == 0 ? Messages.SUCCESSFULLY_INSERTED_RECORD : Messages.SUCCESSFULLY_UPDATED_RECORD;
        }

        protected string GetErrorMessageFromExceptionType(Exception ex)
        {
            var errorMessage = string.Empty;

            if (ex.GetType() == typeof(InvalidDateException))
                errorMessage = Messages.INVALID_DATE;

            else if (ex.GetType() == typeof(DuplicatedEntityException))
                errorMessage = Messages.REGISTER_ALREADY_IN_PLACE;

            else if (ex.GetType() == typeof(CpfException))
                errorMessage = Messages.INVALID_CPF;

            else if (ex.GetType() == typeof(InvalidEmailException))
                errorMessage = Messages.INVALID_EMAIL;

            return errorMessage;
        }

        protected void BuildLoggedUser(Person person, User user, int accessCode)
        {
            user.Login = BuildLoggedUserLogin(person);
            user.Password = BuildLoggedUserPassword(person);
            user.AccessCode = accessCode;
        }

        private static string BuildLoggedUserPassword(Person person)
        {
            return string.Concat(person.Name.RemoveEmptySpaces(), person.BirthDate.Value.Year);
        }

        private static string BuildLoggedUserLogin(Person person)
        {
            return string.Concat(person.Name.RemoveEmptySpaces(), person.User.Login.GetTwoLastCharacters());
        }

        protected string GetFormatedUserLoginAndPassword(Person person)
        {
            var login = "<br/><strong>Login de Acesso : </strong>" + BuildLoggedUserLogin(person);
            login += "<br/><strong>Password : </strong>" + BuildLoggedUserPassword(person);
            return login.ToLower();
        }

        public static string LoggedUserName => LoggerUserProfileName;

        public static int LoggedUserAccessCode => LoggedUserProfileAccessCode;

        public static string GetLoggedUserDescription
        {
            get
            {
                AccessProfileEnum loggedType;
                Enum.TryParse(LoggedUserProfileAccessCode.ToString(), out loggedType);

                return loggedType.GetEnumDescription();
            }
        }

        public List<SelectListItem> BuildListSelectListItemWith<T>(IEnumerable<T> entityList, string optionDescription, string optionValue, string selectedValue = "0")
        {
            var options = new List<SelectListItem> { new SelectListItem { Text = Constants.SELECT, Value = "" } };

            var listItemType = typeof(T);
            var properties = optionDescription.Split('.');

            if (entityList == null) return options;

            foreach (var item in entityList)
            {
                var typeProperty = listItemType.GetProperty(optionDescription);

                string text;

                if (properties.Any())
                {
                    var tipoPropriedade = listItemType.GetProperty(properties[0]).GetValue(item).GetType();
                    typeProperty = tipoPropriedade.GetProperty(properties[1]);
                    text = typeProperty.GetValue(item.GetType().GetProperty(properties[0]).GetValue(item)).ToString();
                }
                else
                    text = typeProperty.GetValue(item).ToString();

                options.Add(BuildSelectListItemWith(optionValue, selectedValue, listItemType, item, text));
            }

            return options;
        }

        public List<SelectListItem> BuildListItemfromEnum<T>(string valorSelecionado)
        {
            return ConvertEnumToListItemWithSelectOption<T>(valorSelecionado);
        }

        public JsonResult BuildJsonObject<T>(List<T> entityList, int total)
        {
            return Json(new
            {
                sEcho = Request.Params["sEcho"],
                iTotalRecords = entityList.Count,
                iTotalDisplayRecords = total,
                ValidateRequest = false,
                aaData = entityList
            }, JsonRequestBehavior.AllowGet);
        }

        private static SelectListItem BuildSelectListItemWith<T>(string valorSelecionado, object value)
        {
            var convertedValue = ((int)value).ToString();

            return new SelectListItem
            {
                Text = ((Enum)value).GetEnumDescription(),
                Value = convertedValue,
                Selected = convertedValue == valorSelecionado
            };
        }

        private static SelectListItem BuildSelectListItemWith<T>(string optionValue, string selectedValue, Type type, T item, string text)
        {
            return new SelectListItem
            {
                Value = type.GetProperty(optionValue).GetValue(item).ToString(),
                Text = text,
                Selected = selectedValue == type.GetProperty(optionValue).GetValue(item).ToString()
            };
        }

        private static List<SelectListItem> ConvertEnumToListItemWithSelectOption<T>(string valorSelecionado)
        {
            var itens = new List<SelectListItem> { new SelectListItem { Text = Constants.SELECT, Value = "" } };
            var enumValues = Enum.GetValues(typeof(T));

            itens.AddRange(from object value in enumValues select BuildSelectListItemWith<T>(valorSelecionado, value));

            return itens;
        }
    }
}
