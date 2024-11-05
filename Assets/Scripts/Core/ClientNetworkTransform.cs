using Unity.Netcode.Components;  // Certifique-se de importar o namespace correto.

public class ClientNetworkTransform : NetworkTransform
{
    // Sobrescreve o método para indicar que o cliente tem autoridade sobre a posição/rotação
    protected override bool OnIsServerAuthoritative()
    {
        return false;  // Retorna false, pois o cliente será o autoritativo
    }
}
