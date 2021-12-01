using System.Collections.Generic;

namespace AfricanObjects.Models
{

    public class Usage
    {
        public string access { get; set; }
    }

    public class Resource
    {
        public int width { get; set; }
        public int height { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public string dimensions { get; set; }
    }

    public class SmithsonianMediumMedium
    {
        public string thumbnail { get; set; }
        public string idsId { get; set; }
        public Usage usage { get; set; }
        public string guid { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public List<Resource> resources { get; set; }
    }

    public class OnlineMedia
    {
        public int mediaCount { get; set; }
        public List<SmithsonianMediumMedium> media { get; set; }
    }

    public class SmithsonianTitle
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class MetadataUsage
    {
        public string access { get; set; }
    }

    public class DescriptiveNonRepeating
    {
        public string record_ID { get; set; }
        public OnlineMedia online_media { get; set; }
        public string unit_code { get; set; }
        public string title_sort { get; set; }
        public string guid { get; set; }
        public string record_link { get; set; }
        public SmithsonianTitle title { get; set; }
        public MetadataUsage metadata_usage { get; set; }
        public string data_source { get; set; }
    }

    public class L3
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class GeoLocation
    {
        public L3 L3 { get; set; }

        public L2 L2 { get; set; }
    }

    public class L2
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class IndexedStructured
    {
        public List<string> date { get; set; }
        public List<GeoLocation> geoLocation { get; set; }
        public List<string> object_type { get; set; }
        public List<string> online_media_rights { get; set; }
        public List<string> name { get; set; }
        public List<string> topic { get; set; }
        public List<string> place { get; set; }
        public List<string> online_media_type { get; set; }
    }

    public class SetName
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class Date
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class Identifier
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class Note
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class ObjectType
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class CreditLine
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class SmithsonianName
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class SmithsonianTopic
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class SmithsonianPlace
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class PhysicalDescription
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class DataSource
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class ObjectRight
    {
        public string label { get; set; }
        public string content { get; set; }
    }

    public class Freetext
    {
        public List<SetName> setName { get; set; }
        public List<Date> date { get; set; }
        public List<Identifier> identifier { get; set; }
        public List<Note> notes { get; set; }
        public List<SmithsonianTitle> title { get; set; }
        public List<ObjectType> objectType { get; set; }
        public List<CreditLine> creditLine { get; set; }
        public List<Name> name { get; set; }
        public List<SmithsonianTopic> topic { get; set; }
        public List<SmithsonianPlace> place { get; set; }
        public List<PhysicalDescription> physicalDescription { get; set; }
        public List<DataSource> dataSource { get; set; }
        public List<ObjectRight> objectRights { get; set; }
    }

    public class Content
    {
        public DescriptiveNonRepeating descriptiveNonRepeating { get; set; }
        public IndexedStructured indexedStructured { get; set; }
        public Freetext freetext { get; set; }
    }

    public class Row
    {
        public string id { get; set; }
        public string title { get; set; }
        public string unitCode { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public Content content { get; set; }
        public string hash { get; set; }
        public string docSignature { get; set; }
        public string timestamp { get; set; }
        public string lastTimeUpdated { get; set; }
        public string version { get; set; }
    }

    public class Response
    {
        public List<Row> rows { get; set; }
        public int rowCount { get; set; }
        public string message { get; set; }
    }

    public class SmithsonianMuseumObject
    {
        public int status { get; set; }
        public int responseCode { get; set; }
        public Response response { get; set; }
    }


}
