for i=0,enemyPartyCount-1 do 
	if enemyParty[i].IsAlive then action = "punch,enemy " + i end
end

if enemyParty[0].IsAlive then action = "punch,enemy 0" end
if enemyParty[1].IsAlive then action = "punch,enemy 1" end

healthDiscrepancy = lowestHealthFriendly.MaxHitPoints - lowestHealthFriendly.HitPoints
print(tostring(healthDiscrepancy))

