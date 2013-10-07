import UnityEngine

class QuitScript (MonoBehaviour): 
		
	def OnMouseUpAsButton():
		Handheld.Vibrate()
		Application.Quit()
