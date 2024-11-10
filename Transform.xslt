<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<!-- Define parameters for filtering -->
	<xsl:param name="name" select="''" />
	<xsl:param name="faculty" select="''" />
	<xsl:param name="department" select="''" />
	<xsl:param name="lab" select="''" />
	<xsl:param name="position" select="''" />
	<xsl:param name="research" select="''" />
	<xsl:param name="customer" select="''" />
	<xsl:param name="customerAddress" select="''" />
	<xsl:param name="subordination" select="''" />
	<xsl:param name="field" select="''" />

	<!-- Define parameters for column visibility -->
	<xsl:param name="showName" select="true()" />
	<xsl:param name="showFaculty" select="true()" />
	<xsl:param name="showDepartment" select="true()" />
	<xsl:param name="showLab" select="true()" />
	<xsl:param name="showPosition" select="true()" />
	<xsl:param name="showResearch" select="true()" />
	<xsl:param name="showCustomer" select="true()" />
	<xsl:param name="showCustomerAddress" select="true()" />
	<xsl:param name="showSubordination" select="true()" />
	<xsl:param name="showField" select="true()" />

	<xsl:template match="/">
		<html>
			<head>
				<style>
					table { border-collapse: collapse; width: 100%; }
					th, td { border: 1px solid black; padding: 8px; text-align: left; }
					th { background-color: #f2f2f2; }
				</style>
			</head>
			<body>
				<table>
					<tr>
						<xsl:if test="$showName='true'">
							<th>Name</th>
						</xsl:if>
						<xsl:if test="$showFaculty='true'">
							<th>Faculty</th>
						</xsl:if>
						<xsl:if test="$showDepartment='true'">
							<th>Department</th>
						</xsl:if>
						<xsl:if test="$showLab='true'">
							<th>Lab</th>
						</xsl:if>
						<xsl:if test="$showPosition='true'">
							<th>Position</th>
						</xsl:if>
						<xsl:if test="$showResearch='true'">
							<th>Research</th>
						</xsl:if>
						<xsl:if test="$showCustomer='true'">
							<th>Customer</th>
						</xsl:if>
						<xsl:if test="$showCustomerAddress='true'">
							<th>Customer Address</th>
						</xsl:if>
						<xsl:if test="$showSubordination='true'">
							<th>Subordination</th>
						</xsl:if>
						<xsl:if test="$showField='true'">
							<th>Field</th>
						</xsl:if>
					</tr>

					<xsl:for-each select="//publication[
                        ($name='' or @NAME=$name) and
                        ($faculty='' or @FACULTY=$faculty) and
                        ($department='' or @DEPARTMENT=$department) and
                        ($lab='' or @LAB=$lab) and
                        ($position='' or @POSITION=$position) and
                        ($research='' or @RESEARCH=$research) and
                        ($customer='' or @CUSTOMER=$customer) and
                        ($customerAddress='' or @CUSTOMER_ADDRESS=$customerAddress) and
                        ($subordination='' or @SUBORDINATION=$subordination) and
                        ($field='' or @FIELD=$field)
                    ]">
						<tr>
							<xsl:if test="$showName='true'">
								<td>
									<xsl:value-of select="@NAME"/>
								</td>
							</xsl:if>
							<xsl:if test="$showFaculty='true'">
								<td>
									<xsl:value-of select="@FACULTY"/>
								</td>
							</xsl:if>
							<xsl:if test="$showDepartment='true'">
								<td>
									<xsl:value-of select="@DEPARTMENT"/>
								</td>
							</xsl:if>
							<xsl:if test="$showLab='true'">
								<td>
									<xsl:value-of select="@LAB"/>
								</td>
							</xsl:if>
							<xsl:if test="$showPosition='true'">
								<td>
									<xsl:value-of select="@POSITION"/>
								</td>
							</xsl:if>
							<xsl:if test="$showResearch='true'">
								<td>
									<xsl:value-of select="@RESEARCH"/>
								</td>
							</xsl:if>
							<xsl:if test="$showCustomer='true'">
								<td>
									<xsl:value-of select="@CUSTOMER"/>
								</td>
							</xsl:if>
							<xsl:if test="$showCustomerAddress='true'">
								<td>
									<xsl:value-of select="@CUSTOMER_ADDRESS"/>
								</td>
							</xsl:if>
							<xsl:if test="$showSubordination='true'">
								<td>
									<xsl:value-of select="@SUBORDINATION"/>
								</td>
							</xsl:if>
							<xsl:if test="$showField='true'">
								<td>
									<xsl:value-of select="@FIELD"/>
								</td>
							</xsl:if>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>