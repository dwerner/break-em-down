import UnityEngine

class Button (MonoBehaviour): 
	
	public defaultMaterial as Material
	
	public highlightMaterial as Material 


	def OnMouseEnter():
		self.renderer.material = highlightMaterial
	
	def OnMouseExit():
		self.renderer.material = defaultMaterial
		