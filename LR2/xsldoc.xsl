<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> 
  <xsl:output
     method="html"></xsl:output>
  <xsl:template match="/">
    <html>
      <body>
	<table border = "1">
          <TR>
	    <th>Company</th>
	    <th>Model</th>
	    <th>Price</th>
	    <th>Rating</th>
	    <th>Quality</th>
	    <th>Size</th>
	  </TR>
	  <xsl:for-each select = "Notebooks/Notebook">
	    <tr>
	      <td>
		<xsl:value-of select = "@Company"/>
	      </td>
	      <td>
		<xsl:value-of select = "@Model"/>
	      </td>
	      <td>
		<xsl:value-of select = "@Price"/>
	      </td>
	      <td>
		<xsl:value-of select = "@Rating"/>
	      </td>
	      <td>
		<xsl:value-of select = "@Quality"/>
	      </td>
	      <td>
		<xsl:value-of select = "@Size"/>
	      </td>
	    </tr>
	  </xsl:for-each>
	</table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>