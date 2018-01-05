using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace RCP.AppFramework
{

    public abstract class LegacyModel
    {
        public abstract byte[] ToByteArray();

        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Makes sure tht the value is Html Encoded before it is sent to Legacy API. 
        /// The decoding is to make sure we prevent double encoding if the value is already encoded.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string PreProcessString(string value)
        {
            return HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(value));
        }
    }

    /// <summary>
    /// XtensibleInfo item
    /// </summary>
    [Serializable]
    public class Field
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class DeleteUser 
    {
        public string UserName { get; set; }
        public string ActivityGUID { get; set; }

    }

    /// <summary>
    /// this model is used as a pass through to the old API to register a user
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "User")]
    public class UserInfo : LegacyModel
    {
        public UserInfo()
        {
            Fields = new Field[] { };
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public dynamic Guid { get; set; }
        public string Degree { get; set; }
        public string EmailAddress { get; set; }
        public string GroupId { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public Field[] Fields { get; set; }
        public OccupationInfo OccupationInfo { get; set; }

        public override byte[] ToByteArray()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<atom:entry xmlns:atom=\"http://www.w3.org/2005/atom\">");
            builder.Append("<atom:category scheme=\"tag:cecity.com,2008/lifetime/schemas#type\" term=\"tag:cecity.com,2008/lifetime/schemas#user\"></atom:category>");
            builder.Append("<atom:content type=\"application/vnd.medbiq.member+xml\"></atom:content>");
            builder.Append("<Members xmlns:a=\"http://ns.medbiq.org/address/v1/\" xmlns:ltd=\"tag:cecity.com,2008:/lifetime/data\" xmlns:n=\"http://ns.medbiq.org/name/v1/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://ns.medbiq.org/member/v1/ http://ns.medbiq.org/member/v1/member.xsd\">");
            builder.Append("<Member restrictions=\"Confidential\" xmlns=\"http://ns.medbiq.org/member/v1/\">");
            builder.AppendFormat("<UniqueID domain=\"tag:aboto.org,2008/\">{0}</UniqueID>", Username);
            builder.AppendFormat("<Password>{0}</Password>", Password);

            builder.Append("<Security>");
            builder.AppendFormat("<SecurityQuestion>{0}</SecurityQuestion>", SecurityQuestion);
            builder.AppendFormat("<SecurityAnswer>{0}</SecurityAnswer>", SecurityAnswer);
            builder.Append("</Security>");

            builder.Append("<Name>");
            builder.AppendFormat("<n:GivenName>{0}</n:GivenName>", FirstName);
            builder.AppendFormat("<n:FamilyName>{0}</n:FamilyName>", LastName);
            builder.AppendFormat("<n:Degree>{0}</n:Degree>", Degree);
            builder.Append("</Name>");

            builder.Append("<Address>");
            builder.AppendFormat("<a:StreetAddressLine>{0}</a:StreetAddressLine>", PreProcessString(Address));
            builder.AppendFormat("<a:StreetAddressLine>{0}</a:StreetAddressLine>", PreProcessString(Address2));
            builder.AppendFormat("<a:City>{0}</a:City>", PreProcessString(City));
            builder.AppendFormat("<a:StateOrProvince>{0}</a:StateOrProvince>", State);
            builder.AppendFormat("<a:PostalCode>{0}</a:PostalCode>", PostalCode);
            builder.Append("<a:Country>");
            builder.AppendFormat("<a:CountryCode>{0}</a:CountryCode>", CountryCode);
            builder.Append("</a:Country>");
            builder.Append("</Address>");

            if (OccupationInfo != null)
            {
                builder.Append("<OccupationInfo>");
                builder.AppendFormat("<Occupation>{0}</Occupation>", string.IsNullOrEmpty(OccupationInfo.Occupation) ? "~~" : OccupationInfo.Occupation);
                builder.AppendFormat("<OccupationTitle>{0}</OccupationTitle>", string.IsNullOrEmpty(OccupationInfo.OccupationTitle) ? "~~" : OccupationInfo.OccupationTitle);
                builder.AppendFormat("<Privileges>{0}</Privileges>", string.IsNullOrEmpty(OccupationInfo.Privileges) ? "~~" : OccupationInfo.Privileges);
                builder.AppendFormat("<Practice>{0}</Practice>", string.IsNullOrEmpty(OccupationInfo.Practice) ? "~~" : OccupationInfo.Practice);
                builder.AppendFormat("<StartDate>{0}</StartDate>", OccupationInfo.StartDate.ToString("YYYY-mm-dd"));
                builder.AppendFormat("<EndDate>{0}</EndDate>", OccupationInfo.EndDate.ToString("YYYY-mm-dd"));

                if (OccupationInfo.Specialties != null && OccupationInfo.Specialties.Any())
                {
                    foreach (var specialty in OccupationInfo.Specialties)
                    {
                        builder.AppendFormat("<Specialty>{0}</Specialty>", specialty);
                    }
                }

                builder.AppendFormat("<OccupationStatus>{0}</OccupationStatus>", OccupationInfo.OccupationStatus);

                builder.Append("</OccupationInfo>");
            }

            builder.Append("<PersonalInfo>");
            builder.AppendFormat("<EmailAddress>{0}</EmailAddress>", EmailAddress);
            builder.Append("</PersonalInfo>");

            builder.Append("<XtensibleInfo>");
            builder.AppendFormat("<ltd:GroupID>{0}</ltd:GroupID>", string.IsNullOrEmpty(GroupId) ? "~~" : GroupId);
            builder.Append("<rdf:RDF>");
            builder.Append("<ltd:ThirdPartyfields>");
            builder.Append("<ltd:Members rdf:parseType=\"Collection\">");

            if (Fields != null && Fields.Any())
            {
                foreach (var field in Fields)
                {
                    if (!string.IsNullOrEmpty(field.Name))
                    {
                        builder.AppendFormat("<ltd:Field ltd:dataType=\"http://www.w3.org/2001/XMLSchema#string\" ltd:name=\"{0}\" ltd:value=\"{1}\"></ltd:Field>",
                            field.Name,
                            PreProcessString(field.Value));
                    }
                }
            }

            builder.Append("</ltd:Members>");
            builder.Append("</ltd:ThirdPartyfields>");
            builder.Append("</rdf:RDF>");
            builder.Append("</XtensibleInfo>");
            builder.Append("</Member>");
            builder.Append("</Members>");
            builder.Append("</atom:entry>");

            byte[] byteArray = Encoding.UTF8.GetBytes(builder.ToString());
            return byteArray;
        }
    }

    /// <summary>
    /// define a users occupations
    /// </summary>
    public class OccupationInfo
    {
        public OccupationInfo()
        {
            Specialties = new string[] { };
        }

        public string Occupation { get; set; }
        public string OccupationTitle { get; set; }
        public string Privileges { get; set; }
        public string Practice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string[] Specialties { get; set; }
        public string OccupationStatus { get; set; }
    }

    public class Participant_Info
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
    }

}
