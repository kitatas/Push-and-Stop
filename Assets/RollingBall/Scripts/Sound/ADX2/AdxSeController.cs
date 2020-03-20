public class AdxSeController : BaseCriAtomSource//, ISeController
{
    public void PlaySe(SeType seType)
    {
        criAtomSource.Play(seType.ToString());
    }
}