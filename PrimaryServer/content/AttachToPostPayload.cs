namespace Primary.Models;

public class AttachToPostPayload
{
    public string? AttachedMaterialBase64 {get; set;}
    public Guid AttachedMaterialId {get; set;}
}