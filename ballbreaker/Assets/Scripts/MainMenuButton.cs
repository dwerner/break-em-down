using System;

namespace AssemblyCSharp {
	public class MainMenuButton	{
		
		public Material defaultMaterial;
		public Material highlightMaterial;


		public void OnMouseEnter(){
			self.renderer.material = highlightMaterial;
		}
		
		public void OnMouseExit(){
			self.renderer.material = defaultMaterial;
		}
		
		public MainMenuButton () {
			
		}
	}
}

