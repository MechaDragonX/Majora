#include <chrono>
#include <ctime>
#include <iostream>
#include <string>
#include <SFML/Audio.hpp>

int main(int argc, char** argv) {
    if(argc != 2) {
        std::cerr << "Not enough arguments!" << std::endl;
        return 1;
    }

    sf::Music music;
    if(!music.openFromFile(argv[1])) {
        // music.~Music();
        return 1;
    }

    // Start music playback on a seperate thread
    music.play();

    std::string input;
    while(true) {
        std::cin >> input;
        if(input == "play") {
            if(music.getStatus() == music.Playing) {
                std::cout << "The music is already playing!" << std::endl;
            } else{
                music.play();
            }
        }
        else if(input == "pause") {
            if(music.getStatus() == music.Paused || music.getStatus() == music.Stopped) {
                std::cout << "The music is already paused or stopped!" << std::endl;
            } else{
                music.pause();
            }
        }
        else if(input == "stop")
            music.stop();
        else if(input == "quit") {
            if(music.getStatus() != music.Stopped)
                music.stop();
            // music.~Music();
            return 0;
        }
        else
            std::cout << "That command is not supoprted!" << std::endl;
    }
}

