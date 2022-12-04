package main

import (
	"Messenger/internal/config"
	"Messenger/internal/transport/http"
	"Messenger/internal/transport/http/handler"
	"context"
	"github.com/rs/zerolog"
	"os"
	"os/signal"
	"syscall"
	"time"
)

func main() {
	out := zerolog.ConsoleWriter{
		Out:        os.Stdout,
		TimeFormat: time.StampMilli,
	}

	logger := zerolog.New(out).With().Caller().Logger().With().Timestamp().Logger()

	cfg, err := config.Parse()
	if err != nil {
		logger.Fatal().Err(err).Msg("failed to parse config")
	}

	handlers := handler.New(logger)

	server := http.New(cfg.Server.Host).WithHealthHandler(handlers)
	go func() {
		if err = server.Run(); err != nil {
			logger.Fatal().Err(err).Msg("failed to start server")
		}
	}()

	quit := make(chan os.Signal, 1)
	signal.Notify(quit, syscall.SIGTERM, syscall.SIGINT)
	<-quit

	logger.Info().Msg("http server shutdown")

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()
	if err := server.Shutdown(ctx); err != nil {
		logger.Fatal().Err(err).Msg("server shutdown error")
	}
}
