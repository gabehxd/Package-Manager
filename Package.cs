using System;

namespace File_Manager
{
    class Package
    {
        public string name;
        public string authors;
        public DateTime created;
        public string packageVersion;
        public string[] files;
        public Uri uri;
        //Future Proofing
        public readonly ulong packageFormatVersion = 1;

        public Package(string name, string authors, DateTime created, string packageVersion, string[] files, Uri uri)
        {
            this.name = name;
            this.authors = authors;
            this.created = created;
            this.packageVersion = packageVersion;
            this.files = files;
            this.uri = uri;
        }
    }
}
