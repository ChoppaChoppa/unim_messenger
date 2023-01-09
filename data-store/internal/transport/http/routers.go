package http

func InitRouters(s *Server) {
	s.GET("/user_data", s.handlers.UserData)
}
