Imports System.Xml.Serialization
Public Class TestSpec
	<XmlElement("TestPhase")>
	Public TestPhaseList As New List(Of TestPhase)
	Public ProductModeId As Integer
	Public Validate As DateTime
	Public Sub New()

	End Sub
	Public Sub New(ByVal cq_modes As CATS.Model.cq_modes)
		Try
			With cq_modes

				Dim cq_phasesManager As New CATS.BLL.cq_phasesManager()
				Dim cq_phasesList As List(Of CATS.Model.cq_phases) = cq_phasesManager.SelectAllByProductModeId(.product_mode_id)


				ProductModeId = cq_modes.product_mode_id
				Validate = cq_modes.validation_date

				If cq_phasesList IsNot Nothing Then


					'TestPhaseList = New List(Of TestPhase)

					For Each ph As CATS.Model.cq_phases In cq_phasesList

						Dim phasename As String = ph.phase.ToUpper

						If phasename Like "IMD*" Or phasename Like "PIM*" Then
							If TestPhaseList Is Nothing Then TestPhaseList = New List(Of TestPhase)
							Dim phase As New TestPhase(ph)
							TestPhaseList.Add(phase)
						End If

					Next
				End If
			End With
		Catch ex As Exception
			Throw New Exception("TestClass.TestSpec.New()::" & ex.Message)
		End Try
	End Sub
	'<Serializable()>
	Public Class TestPhase
		<XmlElement("TestCriteria")>
		Public TestCriteriaList As New Dictionary(Of String, CATS.Model.cq_criteria_detail)

		<XmlElement("TestGroup")>
		Public TestGroupList As New List(Of TestGroup)

		Public PhaseMainId As Integer
		Public SpecMainId As Integer
		Public Name As String
		Public Validate As DateTime
		Public spec_mainM As CATS.Model.spec_main
		Public Sub New()

		End Sub
		Public Sub New(cq_phases As CATS.Model.cq_phases)
			Try
				Dim cq_groupsManager As New CATS.BLL.cq_groupsManager()
				Dim cq_groupsList As List(Of CATS.Model.cq_groups) = cq_groupsManager.SelectAllBySpecMainId(cq_phases.spec_main_id)

				spec_mainM = cq_phases.spec_main_model
				SpecMainId = cq_phases.spec_main_id
				Name = cq_phases.phase
				Validate = cq_phases.validation_date
				PhaseMainId = cq_phases.phase_main_id

				If cq_groupsList IsNot Nothing Then
					'TestGroupList = New List(Of TestGroup)
					For Each cq_gp As CATS.Model.cq_groups In cq_groupsList
						TestGroupList.Add(New TestGroup(cq_gp))
					Next
				End If

				Dim cq_criteria_detailManager As New CATS.BLL.cq_criteria_detailManager
				Dim cq_criteriaList As List(Of CATS.Model.cq_criteria_detail) = cq_criteria_detailManager.SelectAllBySpecMainId(cq_phases.spec_main_id)

				If cq_criteriaList IsNot Nothing Then
					'TestCriteriaList = New Dictionary(Of String, CATS.Model.cq_criteria_detail)
					For Each cq_ct As CATS.Model.cq_criteria_detail In cq_criteriaList
						TestCriteriaList.Add(cq_ct.criteria_item.ToUpper.Trim, cq_ct)
					Next
				End If
			Catch ex As Exception
				Throw New Exception("TestClass.TestPhase.New()::" & ex.Message)
			End Try
		End Sub
		Public Overloads Function ToString()
			Return Name
		End Function
	End Class
	Public Class TestGroup

		<XmlElement("TestItem")>
		Public TestItemList As New List(Of TestItem)
		Public Id As Integer
		Public Name As String
		Public Sub New()

		End Sub
		Public Sub New(cq_group As CATS.Model.cq_groups)
			Try
				With cq_group
					'ID = .group_main_id
					'ValidDate = .validation_date
					'Name = .group_name
					Id = cq_group.group_main_id
					Name = cq_group.group_name

					Dim cq_spec_imd_detailsManager As New CATS.BLL.cq_spec_imd_detailsManager
					Dim cq_spec_imd_detailsList As List(Of CATS.Model.cq_spec_imd_details) = cq_spec_imd_detailsManager.SelectAllByGroupMainId(.group_main_id)

					If cq_spec_imd_detailsList IsNot Nothing Then
						'TestItemList = New List(Of TestItem)

						For Each cq_spec_imd As CATS.Model.cq_spec_imd_details In cq_spec_imd_detailsList
							Dim item As New TestItem(cq_spec_imd)
							TestItemList.Add(item)
						Next
					End If

				End With

			Catch ex As Exception
				Throw New Exception("TestClass.TestGroup.New()::" & ex.Message)
			End Try
		End Sub
	End Class
	'<Serializable()>
	<XmlType("TestItem")>
	Public Class TestItem
		Inherits CATS.Model.cq_spec_imd_details

		Public Sub New()

		End Sub
		Public Sub New(cq_spec_imd_details As CATS.Model.cq_spec_imd_details)
			MyBase.spec_detail = cq_spec_imd_details.spec_detail
			MyBase.spec_imd_detail = cq_spec_imd_details.spec_imd_detail
			MyBase.cfg_imd_main = cq_spec_imd_details.cfg_imd_main
		End Sub
	End Class

End Class

