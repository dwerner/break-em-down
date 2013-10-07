import UnityEngine

class Button (MonoBehaviour): 
	
	public defaultMaterial as Material
	
	public highlightMaterial as Material 

	scaled as Vector3 = Vector3(0,1,0)
			

	def OnMouseOver():
		self.renderer.material = highlightMaterial
		self.transform.localScale += self.scaled
		
	def OnMouseExit():
		self.renderer.material = defaultMaterial
		self.transform.localScale -= self.scaled
		