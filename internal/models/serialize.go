package models

type JsonSerializer struct {
	IVisitor
}

type XmlSerializer struct {
	IVisitor
}

func (json JsonSerializer) VisitStudentAcc(acc Student) string {
	return "ser student to json"
}

func (json JsonSerializer) VisitTeacherAcc(acc Teacher) string {
	return "ser teacher to json"
}

func (xml XmlSerializer) VisitStudentAcc(acc Student) string {
	return "ser student to xml"
}

func (xml XmlSerializer) VisitTeacherAcc(acc Teacher) string {
	return "ser teacher to xml"
}
