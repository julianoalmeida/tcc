﻿using Comum;
using Entidades;
using Entidades.Enumeracoes;
using Entidades.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Comum.Exceptions;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public static int LoggedUserProfileAccessCode { get; set; }
        public static string LoggerUserProfileName { get; set; }

        public const int ERRO = 0;
        public const int SUCESSO = 1;
        public const int REGISTRO_DUPLICADO = 2;
        public const int CAMPO_OBRIGATORIO_NAO_INFORMADO = 3;
        public const int TOTAL_DISCENTES_MAIOR_QUANTIDADE_VAGAS = 4;
        public const int CPF_INVALIDO = 5;
        public const int EMAIL_INVALIDO = 6;
        public const int DATA_ATUAL_FUTURA = 7;

        public ActionResult Index()
        {
            var loggedUser = TempData[Constants.LOGGED_USER] as User;

            LoggedUserProfileAccessCode = loggedUser.AccessCode;
            LoggerUserProfileName = loggedUser.Name;
            TempData.Keep(Constants.LOGGED_USER);

            return RedirectToAction("Index", "Home");
        }

        protected int GetErrorType(int errorCode, Exception ex, ref string msg)
        {
            if (ex.GetType() == typeof(FutureDateException))
            {
                errorCode = DATA_ATUAL_FUTURA;
                msg = Messages.MI003;
            }
            else if (ex.GetType() == typeof(DuplicatedEntityException))
            {
                errorCode = REGISTRO_DUPLICADO;
                msg = Messages.MI009;
            }
            else if (ex.GetType() == typeof(CpfException))
            {
                errorCode = CPF_INVALIDO;
                msg = Messages.MI004;

            }
            else if (ex.GetType() == typeof(EmailException))
            {
                errorCode = EMAIL_INVALIDO;
                msg = Messages.MI006;
            }
            return errorCode;
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
            return string.Concat(person.Name.RemoveEmptySpaces(), person.ZipCode.GetTwoLastCpfCharacters());
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
            var itens = new List<SelectListItem> { new SelectListItem { Text = Constants.SELECT, Value = "" } };

            var type = typeof(T);
            var properties = optionDescription.Split('.');

            if (entityList == null) return itens;

            foreach (var item in entityList)
            {
                var property = type.GetProperty(optionDescription);

                string text;

                if (properties.Any())
                {
                    var tipoPropriedade = type.GetProperty(properties[0]).GetValue(item).GetType();
                    property = tipoPropriedade.GetProperty(properties[1]);
                    text = property.GetValue(item.GetType().GetProperty(properties[0]).GetValue(item)).ToString();
                }
                else
                    text = property.GetValue(item).ToString();

                itens.Add(BuildSelectListItemWith(optionValue, selectedValue, type, item, text));
            }

            return itens;
        }

        public List<SelectListItem> ConvertEnumToListItem<T>(string valorSelecionado)
        {
            return ConvertEnumToListItemWithSelectOption<T>(valorSelecionado);
        }

        public JsonResult BuildJsonObject<T>(List<T> entityList, int total)
        {
            return Json(new
            {
                sEcho = Request.Params["sEcho"],
                iTotalRecords = entityList.Count(),
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