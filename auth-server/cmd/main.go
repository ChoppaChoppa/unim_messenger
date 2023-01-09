package main

import (
	"auth_server/internal/service"
	"auth_server/internal/transport/http"
	"auth_server/internal/transport/http/handlers"
	"github.com/rs/zerolog"
	"os"
	"time"
)

func main() {
	out := zerolog.ConsoleWriter{
		Out:        os.Stdout,
		TimeFormat: time.StampMilli,
	}

	logger := zerolog.New(out).With().Caller().Logger().With().Timestamp().Logger()

	svc := service.New(logger)

	handler := handlers.New(logger, svc)

	server := http.New("127.0.0.1:9000").WithLoginHandlers(handler)
	if err := server.Run(); err != nil {
		logger.Fatal().Err(err).Msg("failed to start server")
	}

}
