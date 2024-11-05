// No assembly Entities
using UnityEngine;

public class ShipAttackSystem
{
    private Transform attackerTransform;

    public ShipAttackSystem(Transform attacker)
    {
        attackerTransform = attacker;
    }

    public void ExecuteAttack(Ship target)
    {
        // Lógica do ataque vai aqui, o EventScreen é gerenciado pelo Ship
        Debug.Log("Iniciando ataque ao navio: " + target.name);
    }
}
