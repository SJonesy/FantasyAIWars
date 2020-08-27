for i=0,enemyPartyCount-1 do 
	if enemyParty[i].IsAlive then action = "punch,enemy " + i end
end

if enemyParty[0].IsAlive then action = "punch,enemy 0" end
if enemyParty[1].IsAlive then action = "punch,enemy 1" end

healthDiscrepancy = lowestHealthFriendly.MaxHitPoints - lowestHealthFriendly.HitPoints
print(tostring(healthDiscrepancy))


# Basic Stick + Hit Melee
meleeAbility = "punch"
if actor.EngagedWith and actor.EngagedWith.IsAlive then
    action = meleeAbility .. ",enemy " .. tostring(actor.CharacterIndex)
    return
end
for i=0,enemyPartyCount-1 do 
    if enemyParty[i].IsAlive then 
        action = meleeAbility .. ",enemy " .. tostring(i)
        return
    end
end