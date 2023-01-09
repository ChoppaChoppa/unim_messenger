package main

import (
	"data-store/internal/service"
	"data-store/internal/transport/http"
	"data-store/internal/transport/http/handlers"
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

	server := http.New("127.0.0.1:9000", handler)
	if err := server.Run(); err != nil {
		logger.Fatal().Err(err).Msg("failed to start server")
	}
}
