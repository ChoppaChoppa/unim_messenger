package http

func InitRouters(s *Server) {
	initLoginRouters(s)
}

func initLoginRouters(s *Server) {
	s.GET("/login", s.login.Login)
}
