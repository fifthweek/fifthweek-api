setlocal

git config --global user.email "services@fifthweek.com"
git config --global user.name "%APPVEYOR_REPO_COMMIT_AUTHOR%"

rmdir /s /q "%APPVEYOR_BUILD_FOLDER%\temp_dist"
rmdir /s /q "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings"

mkdir "%APPVEYOR_BUILD_FOLDER%\temp_dist"
mkdir "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings"

echo Cloning %1
git clone --branch=master https://%git_personal_access_token%:x-oauth-basic@github.com/fifthweek/%1.git "%APPVEYOR_BUILD_FOLDER%\temp_dist"

echo Preserving GIT settings for dist repo
xcopy /e /h "%APPVEYOR_BUILD_FOLDER%\temp_dist\.git" "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings\.git\"

echo Clearing files
rmdir /s /q "%APPVEYOR_BUILD_FOLDER%\temp_dist"
mkdir "%APPVEYOR_BUILD_FOLDER%\temp_dist"

echo Copying new files from %2 to dist repo
xcopy /e /h "%APPVEYOR_BUILD_FOLDER%\%2" "%APPVEYOR_BUILD_FOLDER%\temp_dist\"

echo Re-apply GIT settings for dist repo
xcopy /e /h "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings\.git" "%APPVEYOR_BUILD_FOLDER%\temp_dist\.git\"

echo Changing directory
cd "%APPVEYOR_BUILD_FOLDER%\temp_dist"

echo Pushing new files to %1
git add -A
git commit -m "%APPVEYOR_REPO_COMMIT_MESSAGE% - %APPVEYOR_BUILD_NUMBER%"
git push https://%git_personal_access_token%:x-oauth-basic@github.com/fifthweek/%1.git %APPVEYOR_REPO_BRANCH%
