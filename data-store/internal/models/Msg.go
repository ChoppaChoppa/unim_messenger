package models

type Msg struct {
	IDFrom  int
	IDTo    int
	MsgType int
	Msg     []byte
}
