namespace ShopProjectWebServer.Api.DtoModels.MediaAccessControl
{
    public class MediaAccessControlDto
    {
        /// <summary>
        /// хешоване значення MAC 
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// порядковий номер MAC
        /// </summary>
        public int SequenceNumber { get; set; }
    }
}
