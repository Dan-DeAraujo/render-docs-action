﻿using Scriban;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace D2L.Dev.Docs.Render.Markdown {
	internal class TemplateRenderer {
		private readonly Template m_template;

		public static TemplateRenderer CreateFromResource( string resourceName ) {
			var assembly = Assembly.GetExecutingAssembly();
			using var stream = assembly.GetManifestResourceStream( $"{assembly.GetName().Name}.{resourceName}" );
			using var reader = new StreamReader( stream );
			return new TemplateRenderer( reader.ReadToEnd() );
		}

		public TemplateRenderer( string template ) {
			m_template = Template.Parse( template );
		}

		public async Task<string> RenderAsync( string title, string content ) {
			return await m_template.RenderAsync( new {
				title,
				content
			} );
		}

	}
}
