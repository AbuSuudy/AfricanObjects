using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AfricanObjects.Models
{
    public class HarvardArtMuseum
    {
        public Info info { get; set; }
        public List<Record> records { get; set; }
    }

    public class Info
    {
        public int totalrecordsperquery { get; set; }
        public int totalrecords { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
    }

    public class HarvardImage
    {
        public string date { get; set; }
        public string copyright { get; set; }
        public int imageid { get; set; }
        public int idsid { get; set; }
        public string format { get; set; }
        public object description { get; set; }
        public object technique { get; set; }
        public string renditionnumber { get; set; }
        public int displayorder { get; set; }
        public string baseimageurl { get; set; }
        public object alttext { get; set; }
        public int width { get; set; }
        public object publiccaption { get; set; }
        public string iiifbaseuri { get; set; }
        public int height { get; set; }
    }

    public class Color
    {
        public string color { get; set; }
        public string spectrum { get; set; }
        public string hue { get; set; }
        public double percent { get; set; }
        public string css3 { get; set; }
    }


    public class Record
    {
        public object copyright { get; set; }
        public int contextualtextcount { get; set; }
        public string creditline { get; set; }
        public int accesslevel { get; set; }
        public object dateoflastpageview { get; set; }
        public int classificationid { get; set; }
        public string division { get; set; }
        public int markscount { get; set; }
        public int publicationcount { get; set; }
        public int totaluniquepageviews { get; set; }
        public string contact { get; set; }
        public int colorcount { get; set; }
        public int rank { get; set; }
        public object state { get; set; }
        public int id { get; set; }
        public string verificationleveldescription { get; set; }
        public object period { get; set; }
        public List<HarvardImage> images { get; set; }
        public List<Worktype> worktypes { get; set; }
        public int imagecount { get; set; }
        public int totalpageviews { get; set; }
        public int accessionyear { get; set; }
        public object standardreferencenumber { get; set; }
        public object signed { get; set; }
        public string classification { get; set; }
        public int relatedcount { get; set; }
        public int verificationlevel { get; set; }
        public string primaryimageurl { get; set; }
        public int titlescount { get; set; }
        public int peoplecount { get; set; }
        public object style { get; set; }
        public DateTime lastupdate { get; set; }
        public object commentary { get; set; }
        public object periodid { get; set; }
        public object technique { get; set; }
        public object edition { get; set; }
        public object description { get; set; }
        public string medium { get; set; }
        public int lendingpermissionlevel { get; set; }
        public string title { get; set; }
        public string accessionmethod { get; set; }
        public List<Color> colors { get; set; }
        public object provenance { get; set; }
        public int groupcount { get; set; }
        public object dated { get; set; }
        public string department { get; set; }
        public int dateend { get; set; }
        public string url { get; set; }
        public object dateoffirstpageview { get; set; }
        public string century { get; set; }
        public string objectnumber { get; set; }
        public object labeltext { get; set; }
        public int datebegin { get; set; }
        public string culture { get; set; }
        public int exhibitioncount { get; set; }
        public int imagepermissionlevel { get; set; }
        public int mediacount { get; set; }
        public int objectid { get; set; }
        public object techniqueid { get; set; }
        public object dimensions { get; set; }
        public List<SeeAlso> seeAlso { get; set; }
    }


    public class Exhibition
    {
        public string begindate { get; set; }
        public string enddate { get; set; }
        public string citation { get; set; }
        public int exhibitionid { get; set; }
        public string title { get; set; }
    }

    public class Gallery
    {
        public string begindate { get; set; }
        public string gallerynumber { get; set; }
        public int galleryid { get; set; }
        public string name { get; set; }
        public object theme { get; set; }
        public int floor { get; set; }
    }

    public class Grouping
    {
        public int groupid { get; set; }
        public string name { get; set; }
    }


    public class Mark
    {
        public string text { get; set; }
        public string type { get; set; }
    }



    public class Topic
    {
        public string name { get; set; }
        public int id { get; set; }
    }




    public class Image
    {
        [JsonPropertyName("date")]
        public object Date { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("imageid")]
        public int Imageid { get; set; }

        [JsonPropertyName("idsid")]
        public int Idsid { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("technique")]
        public string Technique { get; set; }

        [JsonPropertyName("renditionnumber")]
        public string Renditionnumber { get; set; }

        [JsonPropertyName("displayorder")]
        public int Displayorder { get; set; }

        [JsonPropertyName("baseimageurl")]
        public string Baseimageurl { get; set; }

        [JsonPropertyName("alttext")]
        public object Alttext { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("publiccaption")]
        public object Publiccaption { get; set; }

        [JsonPropertyName("iiifbaseuri")]
        public string Iiifbaseuri { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class Person
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("birthplace")]
        public object Birthplace { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("displaydate")]
        public object Displaydate { get; set; }

        [JsonPropertyName("prefix")]
        public object Prefix { get; set; }

        [JsonPropertyName("culture")]
        public object Culture { get; set; }

        [JsonPropertyName("displayname")]
        public string Displayname { get; set; }

        [JsonPropertyName("alphasort")]
        public string Alphasort { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("personid")]
        public int Personid { get; set; }

        [JsonPropertyName("deathplace")]
        public object Deathplace { get; set; }

        [JsonPropertyName("displayorder")]
        public int Displayorder { get; set; }
    }

    public class Place
    {
        [JsonPropertyName("displayname")]
        public string Displayname { get; set; }

        [JsonPropertyName("confidence")]
        public object Confidence { get; set; }

        [JsonPropertyName("placeid")]
        public int Placeid { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Publication
    {
        [JsonPropertyName("publicationplace")]
        public string Publicationplace { get; set; }

        [JsonPropertyName("volumetitle")]
        public string Volumetitle { get; set; }

        [JsonPropertyName("citation")]
        public string Citation { get; set; }

        [JsonPropertyName("publicationyear")]
        public int Publicationyear { get; set; }

        [JsonPropertyName("citationremarks")]
        public object Citationremarks { get; set; }

        [JsonPropertyName("pagenumbers")]
        public string Pagenumbers { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("publicationid")]
        public int Publicationid { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("volumenumber")]
        public object Volumenumber { get; set; }

        [JsonPropertyName("publicationdate")]
        public string Publicationdate { get; set; }
    }

    public class Medium
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Place2
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Century
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Culture
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Terms
    {
        [JsonPropertyName("medium")]
        public List<Medium> Medium { get; set; }

        [JsonPropertyName("place")]
        public List<Place2> Place { get; set; }

        [JsonPropertyName("century")]
        public List<Century> Century { get; set; }

        [JsonPropertyName("culture")]
        public List<Culture> Culture { get; set; }
    }

    public class Title
    {
        [JsonPropertyName("titletype")]
        public string Titletype { get; set; }

        [JsonPropertyName("titleid")]
        public int Titleid { get; set; }

        [JsonPropertyName("displayorder")]
        public int Displayorder { get; set; }

        [JsonPropertyName("title")]
        public string TitleString { get; set; }
    }

    public class Worktype
    {
        [JsonPropertyName("worktypeid")]
        public string Worktypeid { get; set; }

        [JsonPropertyName("worktype")]
        public string WorktypeString { get; set; }
    }

    public class SeeAlso
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("profile")]
        public string Profile { get; set; }
    }

    public class RecordMaster
    {
        [JsonPropertyName("objectid")]
        public int Objectid { get; set; }

        [JsonPropertyName("objectnumber")]
        public string Objectnumber { get; set; }

        [JsonPropertyName("accessionyear")]
        public int Accessionyear { get; set; }

        [JsonPropertyName("dated")]
        public string Dated { get; set; }

        [JsonPropertyName("datebegin")]
        public int Datebegin { get; set; }

        [JsonPropertyName("dateend")]
        public int Dateend { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }

        [JsonPropertyName("classificationid")]
        public int Classificationid { get; set; }

        [JsonPropertyName("medium")]
        public string Medium { get; set; }

        [JsonPropertyName("technique")]
        public object Technique { get; set; }

        [JsonPropertyName("techniqueid")]
        public object Techniqueid { get; set; }

        [JsonPropertyName("period")]
        public object Period { get; set; }

        [JsonPropertyName("periodid")]
        public object Periodid { get; set; }

        [JsonPropertyName("century")]
        public string Century { get; set; }

        [JsonPropertyName("culture")]
        public string Culture { get; set; }

        [JsonPropertyName("style")]
        public object Style { get; set; }

        [JsonPropertyName("signed")]
        public object Signed { get; set; }

        [JsonPropertyName("state")]
        public object State { get; set; }

        [JsonPropertyName("edition")]
        public object Edition { get; set; }

        [JsonPropertyName("standardreferencenumber")]
        public object Standardreferencenumber { get; set; }

        [JsonPropertyName("dimensions")]
        public string Dimensions { get; set; }

        [JsonPropertyName("copyright")]
        public object Copyright { get; set; }

        [JsonPropertyName("creditline")]
        public string Creditline { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("division")]
        public string Division { get; set; }

        [JsonPropertyName("contact")]
        public string Contact { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("provenance")]
        public string Provenance { get; set; }

        [JsonPropertyName("commentary")]
        public object Commentary { get; set; }

        [JsonPropertyName("labeltext")]
        public object Labeltext { get; set; }

        [JsonPropertyName("imagecount")]
        public int Imagecount { get; set; }

        [JsonPropertyName("mediacount")]
        public int Mediacount { get; set; }

        [JsonPropertyName("colorcount")]
        public int Colorcount { get; set; }

        [JsonPropertyName("markscount")]
        public int Markscount { get; set; }

        [JsonPropertyName("peoplecount")]
        public int Peoplecount { get; set; }

        [JsonPropertyName("titlescount")]
        public int Titlescount { get; set; }

        [JsonPropertyName("publicationcount")]
        public int Publicationcount { get; set; }

        [JsonPropertyName("exhibitioncount")]
        public int Exhibitioncount { get; set; }

        [JsonPropertyName("contextualtextcount")]
        public int Contextualtextcount { get; set; }

        [JsonPropertyName("groupcount")]
        public int Groupcount { get; set; }

        [JsonPropertyName("relatedcount")]
        public int Relatedcount { get; set; }

        [JsonPropertyName("totalpageviews")]
        public int Totalpageviews { get; set; }

        [JsonPropertyName("totaluniquepageviews")]
        public int Totaluniquepageviews { get; set; }

        [JsonPropertyName("dateoffirstpageview")]
        public string Dateoffirstpageview { get; set; }

        [JsonPropertyName("dateoflastpageview")]
        public string Dateoflastpageview { get; set; }

        [JsonPropertyName("verificationlevel")]
        public int Verificationlevel { get; set; }

        [JsonPropertyName("verificationleveldescription")]
        public string Verificationleveldescription { get; set; }

        [JsonPropertyName("imagepermissionlevel")]
        public int Imagepermissionlevel { get; set; }

        [JsonPropertyName("lendingpermissionlevel")]
        public int Lendingpermissionlevel { get; set; }

        [JsonPropertyName("accesslevel")]
        public int Accesslevel { get; set; }

        [JsonPropertyName("accessionmethod")]
        public string Accessionmethod { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("lastupdate")]
        public DateTime Lastupdate { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("primaryimageurl")]
        public string Primaryimageurl { get; set; }

        [JsonPropertyName("people")]
        public List<Person> People { get; set; }

        [JsonPropertyName("places")]
        public List<Place> Places { get; set; }

        [JsonPropertyName("publications")]
        public List<Publication> Publications { get; set; }

        [JsonPropertyName("terms")]
        public Terms Terms { get; set; }

        [JsonPropertyName("titles")]
        public List<Title> Titles { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("worktypes")]
        public List<Worktype> Worktypes { get; set; }

        [JsonPropertyName("seeAlso")]
        public List<SeeAlso> SeeAlso { get; set; }
    }



}

