var speedoBrowser = null;
let localPlayer = mp.players.local

mp.events.add('Client:Vehicle:freeze', (vehicle, seat) => {
    if (vehicle == null) return

    if (seat === 0){
        if (vehicle.speed < 5.55556) {
            vehicle.setForwardSpeed(0)
        }
    }
});

mp.events.add('Client:Vehicle:Multiplier', (speed) => {
    if (mp.players.local.vehicle) {
        mp.players.local.vehicle.setEnginePowerMultiplier(speed)
    }
})





















// mp.keys.bind(74, true, function() {
//     var fInitialDragCoeff = mp.players.local.vehicle.getHandling("fInitialDragCoeff"),
//     fPercentSubmerged = mp.players.local.vehicle.getHandling("fPercentSubmerged"),
//     fDriveBiasFront = mp.players.local.vehicle.getHandling("fDriveBiasFront"),
//     nInitialDriveGears = mp.players.local.vehicle.getHandling("nInitialDriveGears"),
//     fInitialDriveForce = mp.players.local.vehicle.getHandling("fInitialDriveForce"),
//     fDriveInertia = mp.players.local.vehicle.getHandling("fDriveInertia"),
//     fClutchChangeRateScaleUpShift = mp.players.local.vehicle.getHandling("fClutchChangeRateScaleUpShift"),
//     fClutchChangeRateScaleDownShift = mp.players.local.vehicle.getHandling("fClutchChangeRateScaleDownShift"),
//     fInitialDriveMaxFlatVel = mp.players.local.vehicle.getHandling("fInitialDriveMaxFlatVel"),
//     fBrakeForce = mp.players.local.vehicle.getHandling("fBrakeForce"),
//     fBrakeBiasFront = mp.players.local.vehicle.getHandling("fBrakeBiasFront"),
//     fHandBrakeForce = mp.players.local.vehicle.getHandling("fHandBrakeForce"),
//     fSteeringLock = mp.players.local.vehicle.getHandling("fSteeringLock"),
//     fTractionCurveMax = mp.players.local.vehicle.getHandling("fTractionCurveMax"),
//     fTractionCurveMin = mp.players.local.vehicle.getHandling("fTractionCurveMin"),
//     fTractionCurveLateral = mp.players.local.vehicle.getHandling("fTractionCurveLateral"),
//     fTractionSpringDeltaMax = mp.players.local.vehicle.getHandling("fTractionSpringDeltaMax"),
//     fLowSpeedTractionLossMult = mp.players.local.vehicle.getHandling("fLowSpeedTractionLossMult"),
//     fCamberStiffnesss = mp.players.local.vehicle.getHandling("fCamberStiffnesss"),
//     fTractionBiasFront = mp.players.local.vehicle.getHandling("fTractionBiasFront"),
//     fTractionLossMult = mp.players.local.vehicle.getHandling("fTractionLossMult"),
//     fSuspensionForce = mp.players.local.vehicle.getHandling("fSuspensionForce"),
//     fSuspensionCompDamp = mp.players.local.vehicle.getHandling("fSuspensionCompDamp"),
//     fSuspensionReboundDamp = mp.players.local.vehicle.getHandling("fSuspensionReboundDamp"),
//     fSuspensionUpperLimit = mp.players.local.vehicle.getHandling("fSuspensionUpperLimit"),
//     fSuspensionLowerLimit = mp.players.local.vehicle.getHandling("fSuspensionLowerLimit"),
//     fSuspensionRaise = mp.players.local.vehicle.getHandling("fSuspensionRaise"),
//     fSuspensionBiasFront = mp.players.local.vehicle.getHandling("fSuspensionBiasFront"),
//     fAntiRollBarForce = mp.players.local.vehicle.getHandling("fAntiRollBarForce"),
//     fAntiRollBarBiasFront = mp.players.local.vehicle.getHandling("fAntiRollBarBiasFront"),
//     fRollCentreHeightFront = mp.players.local.vehicle.getHandling("fRollCentreHeightFront"),
//     fRollCentreHeightRear = mp.players.local.vehicle.getHandling("fRollCentreHeightRear"),
//     fCollisionDamageMult = mp.players.local.vehicle.getHandling("fCollisionDamageMult"),
//     nMonetaryValue = mp.players.local.vehicle.getHandling("nMonetaryValue");
//     if(isdriftmodeon == false){
//         if(!mp.players.local.vehicle) return;

//         isdriftmodeon = true

//         mp.players.local.vehicle.setHandling("fInitialDragCoeff", 15.50000);
//         mp.players.local.vehicle.setHandling("fPercentSubmerged", 85.000000);
//         mp.players.local.vehicle.setHandling("fDriveBiasFront", 0.000000);
//         mp.players.local.vehicle.setHandling("nInitialDriveGears", 6);
//         mp.players.local.vehicle.setHandling("fInitialDriveForce", 1.900);
//         mp.players.local.vehicle.setHandling("fDriveInertia", 1.000000);
//         mp.players.local.vehicle.setHandling("fClutchChangeRateScaleUpShift", 1.600000);
//         mp.players.local.vehicle.setHandling("fClutchChangeRateScaleDownShift", 1.600000);
//         mp.players.local.vehicle.setHandling("fInitialDriveMaxFlatVel", (230.000000 / 3.6));
//         mp.players.local.vehicle.setHandling("fBrakeForce", 4.8500000);
//         mp.players.local.vehicle.setHandling("fBrakeBiasFront", (0.670000 * 2));
//         mp.players.local.vehicle.setHandling("fHandBrakeForce", 3.500000);
//         mp.players.local.vehicle.setHandling("fSteeringLock", (52.000000 * 0.017453292));
//         mp.players.local.vehicle.setHandling("fTractionCurveMax", 0.9500);
//         mp.players.local.vehicle.setHandling("fTractionCurveMin", 1.300);
//         mp.players.local.vehicle.setHandling("fTractionCurveLateral", (24.5000 * 0.017453292));
//         mp.players.local.vehicle.setHandling("fTractionSpringDeltaMax", 0.150000);
//         mp.players.local.vehicle.setHandling("fLowSpeedTractionLossMult", 1.000000);
//         mp.players.local.vehicle.setHandling("fCamberStiffnesss", 0.000000);
//         mp.players.local.vehicle.setHandling("fTractionBiasFront", (0.450000 * 2));
//         mp.players.local.vehicle.setHandling("fTractionLossMult", 1.000000);
//         mp.players.local.vehicle.setHandling("fSuspensionForce", 2.500000);
//         mp.players.local.vehicle.setHandling("fSuspensionCompDamp", (2.600000 / 10));
//         mp.players.local.vehicle.setHandling("fSuspensionReboundDamp", (3.00000 / 10));
//         mp.players.local.vehicle.setHandling("fSuspensionUpperLimit", 0.1000);
//         mp.players.local.vehicle.setHandling("fSuspensionLowerLimit", -0.10000);
//         mp.players.local.vehicle.setHandling("fSuspensionRaise", -0.000000);
//         mp.players.local.vehicle.setHandling("fSuspensionBiasFront", (0.500000 * 2));
//         mp.players.local.vehicle.setHandling("fAntiRollBarForce", 0.600000);
//         mp.players.local.vehicle.setHandling("fAntiRollBarBiasFront", (0.540000 * 2));
//         mp.players.local.vehicle.setHandling("fRollCentreHeightFront", 0.390000);
//         mp.players.local.vehicle.setHandling("fRollCentreHeightRear", 0.400000);
//         mp.players.local.vehicle.setHandling("fCollisionDamageMult", 1.000000);
//         mp.players.local.vehicle.setHandling("nMonetaryValue", 30000);
//         mp.players.local.vehicle.setMod(11, 3)

//     } else{
//         isdriftmodeon = false

//         mp.players.local.vehicle.setHandling("fInitialDragCoeff", fInitialDragCoeff);
//         mp.players.local.vehicle.setHandling("fPercentSubmerged", fPercentSubmerged);
//         mp.players.local.vehicle.setHandling("fDriveBiasFront", fDriveBiasFront);
//         mp.players.local.vehicle.setHandling("nInitialDriveGears", nInitialDriveGears);
//         mp.players.local.vehicle.setHandling("fInitialDriveForce", fInitialDriveForce);
//         mp.players.local.vehicle.setHandling("fDriveInertia", fDriveInertia);
//         mp.players.local.vehicle.setHandling("fClutchChangeRateScaleUpShift", fClutchChangeRateScaleUpShift);
//         mp.players.local.vehicle.setHandling("fClutchChangeRateScaleDownShift", fClutchChangeRateScaleDownShift);
//         mp.players.local.vehicle.setHandling("fInitialDriveMaxFlatVel", fInitialDriveMaxFlatVel);
//         mp.players.local.vehicle.setHandling("fBrakeForce", fBrakeForce);
//         mp.players.local.vehicle.setHandling("fBrakeBiasFront", fBrakeBiasFront);
//         mp.players.local.vehicle.setHandling("fHandBrakeForce", fHandBrakeForce);
//         mp.players.local.vehicle.setHandling("fSteeringLock", fSteeringLock);
//         mp.players.local.vehicle.setHandling("fTractionCurveMax", fTractionCurveMax);
//         mp.players.local.vehicle.setHandling("fTractionCurveMin", fTractionCurveMin);
//         mp.players.local.vehicle.setHandling("fTractionCurveLateral", fTractionCurveLateral);
//         mp.players.local.vehicle.setHandling("fTractionSpringDeltaMax", fTractionSpringDeltaMax);
//         mp.players.local.vehicle.setHandling("fLowSpeedTractionLossMult", fLowSpeedTractionLossMult);
//         mp.players.local.vehicle.setHandling("fCamberStiffnesss", fCamberStiffnesss);
//         mp.players.local.vehicle.setHandling("fTractionBiasFront", fTractionBiasFront);
//         mp.players.local.vehicle.setHandling("fTractionLossMult", fTractionLossMult);
//         mp.players.local.vehicle.setHandling("fSuspensionForce", fSuspensionForce);
//         mp.players.local.vehicle.setHandling("fSuspensionCompDamp", fSuspensionCompDamp);
//         mp.players.local.vehicle.setHandling("fSuspensionReboundDamp", fSuspensionReboundDamp);
//         mp.players.local.vehicle.setHandling("fSuspensionUpperLimit", fSuspensionUpperLimit);
//         mp.players.local.vehicle.setHandling("fSuspensionLowerLimit", fSuspensionLowerLimit);
//         mp.players.local.vehicle.setHandling("fSuspensionRaise", fSuspensionRaise);
//         mp.players.local.vehicle.setHandling("fSuspensionBiasFront", fSuspensionBiasFront);
//         mp.players.local.vehicle.setHandling("fAntiRollBarForce", fAntiRollBarForce);
//         mp.players.local.vehicle.setHandling("fAntiRollBarBiasFront", fAntiRollBarBiasFront);
//         mp.players.local.vehicle.setHandling("fRollCentreHeightFront",fRollCentreHeightFront );
//         mp.players.local.vehicle.setHandling("fRollCentreHeightRear", fRollCentreHeightRear);
//         mp.players.local.vehicle.setHandling("fCollisionDamageMult", fCollisionDamageMult);
//         mp.players.local.vehicle.setHandling("nMonetaryValue", nMonetaryValue);
//         mp.players.local.vehicle.setMod(11, 0)

//     }
// });