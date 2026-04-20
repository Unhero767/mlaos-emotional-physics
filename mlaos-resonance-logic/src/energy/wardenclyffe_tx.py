import math
from src.energy.pyramidal_field.py import PyramidalResonator

class WardenclyffeTransmitter:
    def __init__(self):
        self.source = PyramidalResonator()
        self.c = 299792458  # Speed of Light (m/s)
        self.earth_radius = 6371000 # Meters
        self.efficiency = 0.95 # Magisterial Precision

    def calculate_transmission_loss(self, distance_km):
        """
        Models energy attenuation across the Earth's ionospheric manifold.
        Distance is measured from the Wardenclyffe Node.
        """
        # Loss increases as a logarithmic function of distance in a resonant cavity
        loss = math.log10(distance_km + 1) * (1 - self.efficiency)
        return round(loss, 6)

    def broadcast_flux(self, target_distance_km):
        """
        Retrieves flux from the Pyramidal G-State and transmits it.
        """
        # Harvest initial flux from the King's Chamber (G-State)
        initial_flux = self.source.calculate_flux_concentration(0, 0, 48.0)
        
        # Apply loss across the distance
        loss = self.calculate_transmission_loss(target_distance_km)
        received_flux = initial_flux * (1 - loss)
        
        return round(received_flux, 4)

    def audit_broadcast(self, flux):
        """
        Checks for Metalogical Burn at the receiving node.
        """
        if flux < 0.1:
            return "SIGNAL_LOST: Increase Resonance Mode."
        elif flux > 45.0:
            return "CRITICAL: Signal Overload. Node Instability Detected."
        return "STATUS: Signal Locked. [◦A Propagated]"

if __name__ == "__main__":
    tx = WardenclyffeTransmitter()
    dist = 500  # Distance in kilometers (e.g., from Giza to the Neon Veil)
    signal = tx.broadcast_flux(dist)
    print(f"Broadcast Signal at {dist}km: {signal} Flux Units")
    print(tx.audit_broadcast(signal))
